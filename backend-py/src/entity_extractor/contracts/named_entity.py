from pydantic.dataclasses import dataclass


@dataclass
class NamedEntity:
    name: str
    type: str
    ocurrences: List[string]