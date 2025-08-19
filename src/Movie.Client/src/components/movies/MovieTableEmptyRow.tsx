export default function MovieTableEmptyRow() {
    return (
        <tr>
            <td colSpan={6} className="text-center py-12">
                <div className="flex flex-col items-center justify-center">
                    <div className="text-6xl mb-4">🎬</div>
                    <h3 className="text-2xl font-bold mb-2">No movies found</h3>
                    <p className="text-base-content/70 mb-6">Start building your movie collection</p>
                    <button className="btn btn-primary gap-2">
                    <svg 
                        xmlns="http://www.w3.org/2000/svg" 
                        className="h-5 w-5" 
                        viewBox="0 0 20 20" 
                        fill="currentColor"
                    >
                        <path 
                            fillRule="evenodd" 
                            d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" 
                            clipRule="evenodd" 
                        />
                    </svg>
                    Add Your First Movie
                    </button>
                </div>
            </td>
        </tr>
    )
}