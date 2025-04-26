import { useState } from "react";
import axios from 'axios';
import FolderCreator from "./FolderCreator";

export default function FileUploader() {
  const [isFileUploaded, setIsFileUploaded] = useState(false);
  const [file, setFile] = useState(null);

  const handleFileInput = (e) => {
    const files = e.currentTarget.files
    if(files)
    setFile(files[0])    
    setIsFileUploaded(true)
  }

  const handleFileSubmit = async (e) => {
    e.preventDefault()
    const formData = new FormData()
    if (file) {
      formData.append('file', file)
    }
    try {
      const response = await axios.post(`http://localhost:8080/api/v1/documents/upload`, formData, { headers: { "Content-Type": "multipart/form-data" } })
      console.log(response);
     
    } catch (error) {
      console.error(error);
    }
  }

  return (       
    <div className="flex flex-col items-center justify-center">        
      <form onSubmit={handleFileSubmit}>      
        <div className="flex flex-col gap-y-4">
        <div className="text-lg font-medium">Nahrání souboru</div>         
          <div className="flex items-center justify-center w-full">
            <label
              htmlFor="dropzone-file"
              className="flex flex-col items-center justify-center p-3 w-full h-36 border-2 border-gray-300 border-dashed rounded-lg cursor-pointer bg-[#ffffff] hover:bg-[#f9f9f9]"
            >
              <div className="flex flex-col items-center justify-center pt-5 pb-6">
                <svg
                  className="w-8 h-8 mb-4 text-[#7b7b7b] dark:text-[#9b9b9b]"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 20 16"
                >
                  <path
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M13 13h3a3 3 0 0 0 0-6h-.025A5.56 5.56 0 0 0 16 6.5 5.5 5.5 0 0 0 5.207 5.021C5.137 5.017 5.071 5 5 5a4 4 0 0 0 0 8h2.167M10 15V6m0 0L8 8m2-2 2 2"
                  />
                </svg>
                <p className="mb-2 text-sm text-[#7b7b7b] dark:text-[#9b9b9b]">
                  <span className="font-semibold">Nahrát soubor</span> nebo
                  přetáhnout
                </p>
                <p className="text-xs text-[#7b7b7b] dark:text-[#9b9b9b]">
                  pdf (MAX. Lorem ipsum MB)
                </p>
              </div>
              <input
                id="dropzone-file"
                type="file"
                className="hidden"
                accept=".pdf"
                required
                onChange={handleFileInput}
              />
            </label>
          </div>
          <FolderCreator />
          <button className="py-3 w-full bg-blue-500 hover:bg-blue-600 text-white rounded-lg text-center cursor-pointer">
            Potvrdit
          </button>
          {isFileUploaded && <div className="flex flex-col gap-y-1 w-full">
            <p className="text-center text-blue-800 text-sm">Soubor nahraný</p>
            <div className="h-1.5 w-full bg-green-500 rounded-full"></div>
          </div>}
        </div>
      </form>
    </div>
  );
}
