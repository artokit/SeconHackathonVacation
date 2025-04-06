import uvicorn
from fastapi import FastAPI
from routers.email_notification import router as email_notification_router

app = FastAPI()
app.include_router(email_notification_router)


if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=8001)
