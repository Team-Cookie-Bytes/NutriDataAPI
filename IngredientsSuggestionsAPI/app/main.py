from typing import List
from fastapi import FastAPI, File, UploadFile
from pydantic import BaseModel

app = FastAPI()


class IngredientSuggestion(BaseModel):
    ingredient: str
    probability: float


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.post("/ingredients-suggestions")
async def get_ingredients_suggestions(
    img: UploadFile = File(...),
) -> List[IngredientSuggestion]:
    print(img.filename)
    return [
        IngredientSuggestion(ingredient="apple", probability=0.5),
        IngredientSuggestion(ingredient="banana", probability=0.4),
        IngredientSuggestion(ingredient="carrot", probability=0.1),
    ]
