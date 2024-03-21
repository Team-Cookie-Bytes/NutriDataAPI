from typing import List
from fastapi import FastAPI, File, UploadFile
from pydantic import BaseModel
from .predictor import Predictor

app = FastAPI()
predictor = Predictor()


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.post("/ingredients-suggestions")
async def get_ingredients_suggestions(
    img: UploadFile = File(...),
) -> List[str]:
    return ["apple", "banana", "cherry"]
    # return predictor.predict(img)
