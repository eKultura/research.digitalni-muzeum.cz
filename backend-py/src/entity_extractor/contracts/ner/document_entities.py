from pydantic.dataclasses import dataclass


@dataclass
class DocumentEntities:
    id: str
    name: str
    project: str
