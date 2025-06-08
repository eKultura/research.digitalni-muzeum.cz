from abc import ABC, abstractmethod


class EntityRecognizer(ABC):

    @abstractmethod
    def recognize(self):
        pass
