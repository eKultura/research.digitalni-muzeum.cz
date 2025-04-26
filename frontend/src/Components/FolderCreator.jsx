import { useState, useEffect, useRef } from 'react';
import { Folder, FolderPlus, ChevronDown, ChevronRight, Check } from 'lucide-react';


export default function FolderCreator() {

    const [folders, setFolders] = useState([
        { id: 1, name: 'Lorem Ipsum 1' },
        { id: 2, name: 'Lorem Ipsum 2' },
        { id: 3, name: 'Lorem Ipsum 3' }
      ]);
      
      const [selectedFolder, setSelectedFolder] = useState(null);
      const [isOpen, setIsOpen] = useState(false);
      const [newFolderMode, setNewFolderMode] = useState(false);
      const [newFolderName, setNewFolderName] = useState('');      
     
      const dropdownRef = useRef(null);
    
      useEffect(() => {
        const handleClickOutside = (event) => {
          if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
            setIsOpen(false);
            if (newFolderMode) {
              setNewFolderMode(false);
            }
          }
        };
    
        
        if (isOpen) {
          document.addEventListener('mousedown', handleClickOutside);
        }        
        
        return () => {
          document.removeEventListener('mousedown', handleClickOutside);
        };
      }, [isOpen, newFolderMode]);
    
      const handleCreateFolder = () => {
        if (newFolderName.trim() === '') return;
        
        const newFolder = {
          id: folders.length + 1,
          name: newFolderName.trim()
        };
        
        setFolders([...folders, newFolder]);
        setSelectedFolder(newFolder);
        setNewFolderName('');
        setNewFolderMode(false);
      };
    
      const toggleDropdown = () => setIsOpen(!isOpen);
      
      return (
        <div className="w-72 relative" ref={dropdownRef}>
          <div className="mb-2 text-lg font-medium">Výběr složky *</div>         
          <div 
            className="flex items-center justify-between p-3 border rounded-md bg-white cursor-pointer hover:bg-gray-50"
            onClick={toggleDropdown}
          >
            <div className="flex items-center">
              {selectedFolder ? (
                <>
                  <Folder className="mr-2 text-blue-500" size={20} />
                  <span>{selectedFolder.name}</span>
                </>
              ) : (
                <span className="text-gray-500">Zvolit složku</span>
              )}
            </div>
            <div>
              {isOpen ? <ChevronDown size={20} /> : <ChevronRight size={20} />}
            </div>
          </div>          
         
          {isOpen && (
            <div className="absolute z-10 w-full mt-1 bg-white border rounded-md shadow-lg">              
              <div className="max-h-48 overflow-y-auto">
                {folders.map(folder => (
                  <div 
                    key={folder.id}
                    className={`flex items-center justify-between p-3 cursor-pointer hover:bg-gray-100 ${
                      selectedFolder?.id === folder.id ? 'bg-blue-50' : ''
                    }`}
                    onClick={() => {
                      setSelectedFolder(folder);
                      setIsOpen(false);
                    }}
                  >
                    <div className="flex items-center">
                      <Folder className="mr-2 text-blue-500" size={20} />
                      <span>{folder.name}</span>
                    </div>
                    {selectedFolder?.id === folder.id && (
                      <Check size={16} className="text-blue-500" />
                    )}
                  </div>
                ))}
              </div>
              
              <div className="border-t">
                {newFolderMode ? (
                  <div className="p-3">
                    <div className="flex items-center mb-2">
                      <FolderPlus className="mr-2 text-green-500" size={20} />
                      <span className="font-medium">Nová složka</span>
                    </div>
                    <div className="flex">
                      <input
                        type="text"
                        className="flex-1 border rounded-l-md p-2 text-sm focus:outline-none focus:ring-1 focus:ring-blue-500"
                        placeholder="Folder name"
                        value={newFolderName}
                        onChange={(e) => setNewFolderName(e.target.value)}
                        autoFocus
                      />
                      <button
                        className="bg-blue-500 text-white px-3 py-2 rounded-r-md text-sm hover:bg-blue-600"
                        onClick={handleCreateFolder}
                      >
                        Vytvořit
                      </button>
                    </div>
                    <div className="flex justify-end mt-2">
                      <button
                        className="text-sm text-gray-500 hover:text-gray-700"
                        onClick={() => setNewFolderMode(false)}
                      >
                        Zrušit
                      </button>
                    </div>
                  </div>
                ) : (
                  <div
                    className="flex items-center p-3 cursor-pointer hover:bg-gray-100"
                    onClick={() => setNewFolderMode(true)}
                  >
                    <FolderPlus className="mr-2 text-green-500" size={20} />
                    <span>Vytvořit novou složku</span>
                  </div>
                )}
              </div>
            </div>
          )}
        </div>
      );
}