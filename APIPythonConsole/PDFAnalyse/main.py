# For run >> uvicorn app:app --reload
import uvicorn
import asyncio
from fastapi import FastAPI, File, UploadFile
from llama_index import GPTVectorStoreIndex, SimpleDirectoryReader, LLMPredictor, ServiceContext
from langchain.chat_models import ChatOpenAI
import os
from fastapi.middleware.cors import CORSMiddleware
#from decouple import config
from dotenv import load_dotenv
import openai

#load_dotenv()  # Carga las variables de entorno desde el archivo .env
load_dotenv(encoding='utf-8-sig')

print("Iniciando API...")

app = FastAPI() 

api_key = os.getenv('OPENAI_API_KEY')

openai.api_key = api_key
#os.environ["OPENAI_API_KEY"] = api_key 

# Agregar el middleware de CORS a la aplicación
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

#Leer los PDFs
#pdf = SimpleDirectoryReader('Data').load_data()

#Definir e instanciar el modelo
modelo = LLMPredictor(llm=ChatOpenAI(temperature=0, model_name='gpt-3.5-turbo'))

print("Modelo cargado...")

#Indexar el contenido de los PDFs
service_context = ServiceContext.from_defaults(llm_predictor=modelo)
#index = GPTVectorStoreIndex.from_documents(pdf, service_context=service_context)

print("Indexado terminado...")

#Guardar el índice a disco para no tener que repetir cada vez
#Recordar que necesistaríamos persistir el drive para que lo mantenga
#index.save_to_disk('index.json')

#Cargar el índice del disco
#index = GPTVectorStoreIndex.load_from_disk('index.json')

@app.get("/")
async def test():
    return {"response": "Hello World!"}

@app.get("/ask/")
async def responder_pregunta(question: str):
    # Leer los PDFs
    pdf = SimpleDirectoryReader('Data').load_data()
    _index = GPTVectorStoreIndex.from_documents(pdf, service_context=service_context)

    response = _index.as_query_engine().query(question)
    return {"response": response.response}

@app.post("/uploadfile/")
async def create_upload_file(file: UploadFile = File(...)):
    with open(os.path.join("Data", file.filename), "wb") as buffer:
        buffer.write(await file.read())

    # Leer el archivo PDF recién agregado
    # pdf_new = SimpleDirectoryReader('Data/'+file.filename).load_data()

    # Agregar el nuevo documento al índice del modelo
    # index.insert(pdf_new, service_context=service_context)

    return {"filename": file.filename}

async def main():
    config = uvicorn.Config("main:app", port=5167, log_level="info")
    server = uvicorn.Server(config)
    await server.serve()

if __name__ == "__main__":
    asyncio.run(main())
