from dataclasses import dataclass


@dataclass
class AzureConfiguration:

    endpoint: str
    api_key: str
