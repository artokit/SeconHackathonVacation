from pydantic import BaseModel, EmailStr
from enum import Enum
import json


class MailType(Enum):
    Password = 1
    RequestToDirector = 2
    AnswerForRequest = 3


class EmailNotificationRequest(BaseModel):
    to_email: EmailStr


class GeneratedPasswordRequest(EmailNotificationRequest):
    password: str

    def to_json(self) -> str:
        return json.dumps({
            "to_email": self.to_email,
            "password": self.password
        })
