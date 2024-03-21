from typing import List
from fastapi import FastAPI, File, Form, UploadFile
from pydantic import BaseModel

app = FastAPI()


class MassPrediction(BaseModel):
    ingredients: str
    mass: float


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.post("/mass-prediction")
async def get_mass_prediction(
    img: UploadFile = File(...),
    ingredients: List[str] = Form(...),
) -> MassPrediction:
    print(img.filename)
    print(ingredients)
    return List[MassPrediction(mass=13)]
