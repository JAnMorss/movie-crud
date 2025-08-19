import { useEffect, useState } from "react";
import type { MovieDto } from "../../models/movieDto.ts";
import apiConnector from "../../api/apiConnector";
import MovieTableItem from "./MovieTableItem.tsx";
import MovieTableEmptyRow from "./MovieTableEmptyRow.tsx";
import { FaPlus } from "react-icons/fa";
import { Link } from "react-router-dom";

export default function MovieTable() {
  const [movies, setMovies] = useState<MovieDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const fetchedMovies = await apiConnector.getMovies();
        setMovies(fetchedMovies);
        setError(null);
      } catch (err) {
        console.error("Error fetching movies:", err);
        setError(`Failed to fetch movies: ${err instanceof Error ? err.message : 'Unknown error'}`);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleRetry = () => {
    setLoading(true);
    setError(null);
    const fetchData = async () => {
      try {
        const fetchedMovies = await apiConnector.getMovies();
        setMovies(fetchedMovies);
        setError(null);
      } catch (err) {
        console.error("Error on retry:", err);
        setError(`Failed to fetch movies: ${err instanceof Error ? err.message : 'Unknown error'}`);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  };

  if (loading) {
    return (
      <div className="hero min-h-screen bg-base-200">
        <div className="hero-content text-center">
          <span className="loading loading-spinner loading-lg text-primary"></span>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-base-200 p-4">
      <div className="container mx-auto max-w-7xl">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-center mb-2">Movie Collection</h1>
          <p className="text-center text-base-content/70">Manage your favorite movies</p>
        </div>

        {/* Main Card */}
        <div className="card bg-base-100 shadow-xl">
          <div className="card-body">
            
            <div className="flex justify-between items-center mb-6">
              <h2 className="card-title text-2xl">Movies ({movies.length})</h2>
              <Link to={"createMovie"} className="btn btn-primary gap-2 hover:bg-violet-600">
                <FaPlus /> Create Movie
              </Link>
            </div>

            {/* Table Container */}
            <div className="overflow-x-auto">
              <table className="table table-zebra w-full">
                <thead>
                  <tr className="bg-primary text-primary-content">
                    <th className="text-center">ID</th>
                    <th className="text-center">Title</th>
                    <th className="text-center">Description</th>
                    <th className="text-center">Category</th>
                    <th className="text-center">Created</th>
                    <th className="text-center">Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {error ? (
                    <tr>
                      <td colSpan={6} className="text-center py-12">
                        <div className="alert alert-error max-w-md mx-auto">
                          <svg xmlns="http://www.w3.org/2000/svg" className="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />
                          </svg>
                          <div>
                            <h3 className="font-bold">Error!</h3>
                            <div className="text-xs">{error}</div>
                          </div>
                        </div>
                        <button className="btn btn-outline btn-error mt-4" onClick={handleRetry}>
                          <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15" />
                          </svg>
                          Retry
                        </button>
                      </td>
                    </tr>
                  ) : movies.length > 0 ? (
                    movies.map((movie, index) => (
                      <MovieTableItem key={index} movie={movie} />
                    ))
                  ) : (
                    <MovieTableEmptyRow />
                  )}
                  
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}