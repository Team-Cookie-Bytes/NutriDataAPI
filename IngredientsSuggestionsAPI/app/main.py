from typing import List
from fastapi import FastAPI
from pydantic import BaseModel
import base64
import io
from PIL import Image
import numpy as np
from .predictor import Predictor

app = FastAPI()
predictor = Predictor()


class Body(BaseModel):
    base64image: str


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.post("/ingredients-suggestions")
async def get_ingredients_suggestions(body: Body) -> List[str]:
    imgdata = base64.b64decode(body.base64image)
    img = np.array(Image.open(io.BytesIO(imgdata)))
    print(img.shape)
    # return predictor.predict(img)
    return ["apple", "banana", "cherry"]
