from fastapi import APIRouter, UploadFile, File, Form

router = APIRouter()


@router.post("/upload", status_code=201)
async def upload_document(document: UploadFile = File(...),
                          project: str = Form()):
    """
    Processes and stores a text document
    :param document: text document
    :param project: project the document belongs to
    :return:
    """

    pass


@router.get("documents/{document_id}")
async def get_document(document_id: str):
    """
    Gets the document with the given ID
    :param document_id: id of a document
    :return:
    """

    pass