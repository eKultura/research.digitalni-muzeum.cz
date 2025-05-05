import { NavLink } from "react-router";

export default function Navbar() {
    return (
        
<nav className="bg-gray-200 shadow shadow-gray-300 px-8 md:px-auto">
	<div className="md:h-16 h-28 mx-auto md:px-4 container flex items-center justify-between flex-wrap md:flex-nowrap">
		
		<div className="text-indigo-500 md:order-1">
			
			
		</div>
		<div className="text-blue-500 order-3 w-full md:w-auto md:order-2">
			<ul class="flex font-semibold justify-between">              
				
				<NavLink to="/" className="md:px-4 md:py-2 hover:text-blue-400">Přidat dokumenty</NavLink>
				<NavLink to="/documents" className="md:px-4 md:py-2 hover:text-blue-400">Přehled dokumentů</NavLink>
                <NavLink to="/searching" className="md:px-4 md:py-2 hover:text-blue-400">Hledat souvislosti</NavLink>
			</ul>
		</div>
		{/* <div className="order-2 md:order-3">
			<button className="px-4 py-2 bg-blue-500 hover:bg-blue-600 text-gray-50 rounded-xl flex items-center gap-2">
                
                <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M3 3a1 1 0 011 1v12a1 1 0 11-2 0V4a1 1 0 011-1zm7.707 3.293a1 1 0 010 1.414L9.414 9H17a1 1 0 110 2H9.414l1.293 1.293a1 1 0 01-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0z" clipRule="evenodd" />
                </svg>
                <span>Prihlasit</span>
            </button>
		</div> */}
	</div>
</nav>
    )
};