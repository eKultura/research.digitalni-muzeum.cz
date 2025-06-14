import string
from typing import List

from pydantic.dataclasses import dataclass


@dataclass
class NamedEntity:
    name: str
    type: str
    occurrences: List[string]
