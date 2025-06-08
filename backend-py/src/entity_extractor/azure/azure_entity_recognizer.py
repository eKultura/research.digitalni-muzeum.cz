from azure.ai.textanalytics import TextAnalyticsClient
from azure.core.credentials import AzureKeyCredential

from azure.azure_configuration import AzureConfiguration
from contracts.ner.entity_recognizer import EntityRecognizer
from contracts.text_document import TextDocument


class AzureEntityRecognizer(EntityRecognizer):

    _config: AzureConfiguration
    _azure_client: TextAnalyticsClient

    def __init__(self, config: AzureConfiguration):
        self._config = config

        credential = AzureKeyCredential(config.api_key)
        self._azure_client = TextAnalyticsClient(endpoint=config.endpoint, credential=credential)

    def recognize(self,  text_document: TextDocument):
        pass