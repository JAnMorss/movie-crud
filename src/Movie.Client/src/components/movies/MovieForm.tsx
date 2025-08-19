import { useEffect, useState, type ChangeEvent } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import type { MovieDto } from "../../models/movieDto";
import apiConnector from "../../api/apiConnector";

interface ValidationError {
  propertyName: string;
  errorMassage: string; 
}

interface ValidationResponse {
  type: string;
  title: string;
  status: number;
  detail: string;
  errors: ValidationError[];
}


export default function MovieForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [movie, setMovie] = useState<MovieDto>({
    id: undefined,
    title: "",
    description: "",
    createDate: undefined,
    category: "",
  });

  const [validationErrors, setValidationErrors] = useState<ValidationError[]>([]);

  useEffect(() => {
    if (id) {
      apiConnector.getMovieById(id).then((movie) => setMovie(movie!));
    }
  }, [id]);

 async function handleSubmit(e: React.FormEvent) {
  e.preventDefault();

  try {
    if (!movie.id) {
      await apiConnector.createMovie(movie);
      navigate("/");
    } else {
      await apiConnector.editMovie(movie);
      navigate("/");
    }
  } catch (error: any) {
    if (error.response?.status === 400) {
      const data: ValidationResponse = error.response.data;
      setValidationErrors(data.errors);
    }
  }
}


  function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    const { name, value } = event.target;
    setMovie({...movie, [name]: value });
  }

  return (
    <div className="mr-40 max-w-2xl mx-auto mt-12 p-8 bg-base-100 rounded-xl shadow-lg border border-gray-200">
        <div className="text-center mb-8">
            <div className="inline-flex items-center justify-center w-16 h-16 bg-primary/10 rounded-2xl mb-4">
                <svg className="w-8 h-8 text-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 4v16l13-8L7 4z" />
                </svg>
            </div>
            <h1 className="text-4xl font-bold bg-gradient-to-r from-primary to-secondary bg-clip-text text-transparent mb-2">
                {movie.id ? "Edit Movie" : "Create Movie"}
            </h1>
            <p className="text-base-content/60 text-lg">
                {movie.id ? "Update your movie details" : "Add a new movie to your collection"}
            </p>
        </div>

        <form onSubmit={handleSubmit} autoComplete="off" className="space-y-6">
            <div className="form-control w-full">
                <label className="label">
                    <span className="label-text font-medium text-gray-700">Title</span>
                </label>
                <input
                    type="text"
                    name="title"
                    placeholder="Enter movie title"
                    value={movie.title}
                    onChange={handleInputChange}
                    className="input input-bordered w-full"
                />
                {validationErrors
                    .filter((err) => err.propertyName === "Title")
                    .map((err, idx) => (
                    <p key={idx} className="text-error text-sm mt-1">
                        {err.errorMassage}
                    </p>
                    ))}
            </div>

            <div className="form-control w-full">
                <label className="label">
                <span className="label-text font-medium text-gray-700">
                    Description
                </span>
                </label>
                <textarea
                name="description"
                placeholder="Enter movie description"
                value={movie.description}
                onChange={handleInputChange}
                className="textarea textarea-bordered w-full"
                rows={4}
                />
                {validationErrors
                .filter((err) => err.propertyName === "Description")
                .map((err, idx) => (
                    <p key={idx} className="text-error text-sm mt-1">
                    {err.errorMassage}
                    </p>
                ))}
            </div>

            <div className="form-control w-full">
                <label className="label">
                <span className="label-text font-medium text-gray-700">Category</span>
                </label>
                <input
                type="text"
                name="category"
                placeholder="Enter category"
                value={movie.category}
                onChange={handleInputChange}
                className="input input-bordered w-full"
                />
                {validationErrors
                .filter((err) => err.propertyName === "Category")
                .map((err, idx) => (
                    <p key={idx} className="text-error text-sm mt-1">
                    {err.errorMassage}
                    </p>
                ))}
            </div>

            <div className="flex justify-end gap-4 mt-4">
                <button
                type="submit"
                className="btn btn-primary hover:btn-secondary transition-all"
                >
                Submit
                </button>
                <Link
                to="/"
                className="btn btn-outline hover:bg-gray-100 transition-all"
                >
                Cancel
                </Link>
            </div>
        </form>
    </div>
  );
}
