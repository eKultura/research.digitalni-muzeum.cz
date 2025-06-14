from pydantic.v1 import BaseSettings


class AzureConfiguration(BaseSettings):
    endpoint: str
    api_key: str
