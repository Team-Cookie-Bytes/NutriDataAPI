from typing import List
from fastapi import FastAPI, File, UploadFile
from pydantic import BaseModel
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
    print(body.base64image)
    return ["apple", "banana", "cherry"]
    # return predictor.predict(img)
