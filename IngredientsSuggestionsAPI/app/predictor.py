from typing import List, Tuple
import torchvision
import torch
from torch import load
import pickle
from sklearn.preprocessing import MultiLabelBinarizer
import pandas as pd


class Predictor:
    def __init__(self, num_classes=247):
        self.model = torchvision.models.mobilenet_v3_small()

        num_ftrs = self.model.classifier[3].in_features
        self.model.classifier[3] = torch.nn.Linear(num_ftrs, num_classes)

        with open("./state/model_state.pt", "rb") as f:
            self.model.load_state_dict(load(f))

        with open("./state/mlb.pkl", "rb") as f:
            self.mlb: MultiLabelBinarizer = pickle.load(f)

        self.df = pd.read_csv("./state/ingredients_metadata.csv")

    def predict(self, img=None, topk=5) -> List[Tuple[str, float]]:
        tensor = torch.FloatTensor(img)[None, :, :, :]
        # print(tensor.shape)

        output = self.model(tensor)
        # print(output)

        probabilities = torch.nn.functional.softmax(output, dim=1)[0]
        # print(probabilities)

        top_probs, indices = torch.topk(probabilities, topk)
        # print(indices)
        # print(top_probs.tolist())

        # Create a zeros tensor of the same shape
        mask = torch.zeros_like(probabilities)
        # print(mask)

        # Set the top 1 position to 1
        mask[indices] = 1
        # print(mask.view(1, -1).shape[1])

        ingredient_ids = self.mlb.inverse_transform(mask.view(1, -1))[0]
        # print(ingredient_ids)

        ingredient_names = [
            self.df.loc[self.df["id"] == id, "ingr"].values[0]
            for id in list(ingredient_ids)
        ]
        # print(ingredient_names)

        return list(zip(ingredient_names, top_probs.tolist()))
