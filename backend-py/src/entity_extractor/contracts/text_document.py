from dataclasses import dataclass
from typing import List

from contracts.text_document_page import TextDocumentPage


@dataclass
class TextDocument:
    name: str
    project: str
    pages: List[TextDocumentPage]
