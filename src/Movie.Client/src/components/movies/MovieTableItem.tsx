import { FaRegEdit } from "react-icons/fa";
import apiConnector from "../../api/apiConnector";
import type { MovieDto } from "../../models/movieDto";
import { MdOutlineDelete } from "react-icons/md";
import { Link } from "react-router-dom";

interface Props {
  movie: MovieDto;
}

export default function MovieTableItem({ movie }: Props) {
  const handleDelete = async () => {
    await apiConnector.deleteMovie(movie.id!);
    window.location.reload();
  };

  return (
    <tr className="hover:bg-base-200 transition-colors">
      <td className="text-center">{movie.id}</td>
      <td className="text-center">
        <div className="font-semibold">
          {movie.title}
        </div>
      </td>
      <td className="text-center">{movie.description}</td>
      <td className="text-center">
        <div className="badge badge-outline badge-primary">
          {movie.category || 'N/A'}
        </div>
      </td>
      <td className="text-center">{movie.createDate}</td>
      <td className="flex justify-center gap-2 py-2">
        <Link to={`/editMovie/${movie.id}`} className="btn btn-warning btn-sm btn-outline gap-1" >
          <FaRegEdit /> Edit
        </Link>
        <button
          className="btn btn-error btn-sm btn-outline gap-1"
          onClick={handleDelete}
        >
          <MdOutlineDelete /> Delete
        </button>
      </td>
    </tr>
  );
}
