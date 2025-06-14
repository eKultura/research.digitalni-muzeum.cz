from abc import ABC, abstractmethod

from contracts.text_document import TextDocument


class EntityRecognizer(ABC):


    @abstractmethod
    def recognize(self, text_document: TextDocument):
        pass
