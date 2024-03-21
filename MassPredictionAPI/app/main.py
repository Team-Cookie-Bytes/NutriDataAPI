from typing import List
from fastapi import FastAPI, File, Form, UploadFile
from pydantic import BaseModel

app = FastAPI()


class MassPrediction(BaseModel):
    mass: float


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.post("/mass-prediction")
async def get_mass_prediction(
    img: UploadFile = File(...),
    ingredient: str = Form(...),
) -> MassPrediction:
    print(img.filename)
    print(ingredient)
    return MassPrediction(mass=13)
