import type { AxiosResponse } from "axios";
import type { MovieDto } from "../models/movieDto";
import axios from "axios";
import { API_BASE_URL } from "../../config.ts";

const apiConnector = {

    getMovies: async (): Promise<MovieDto[]> => {
        try {
            const response: AxiosResponse<MovieDto[]> = 
                await axios.get(`${API_BASE_URL}/movie`);
            
            const movies = response.data.map(movie => ({
                ...movie,
                createDate: movie.createDate?.slice(0, 10) ?? ""
            }));
            return movies
        } catch (error) {
            console.log("Error fetching movies:", error);
            throw error;
        }
    },

    getMovieById: async (movieId: string): Promise<MovieDto | undefined> => {
        try {
            const response = await axios.get<MovieDto>(`${API_BASE_URL}/movie/${movieId}`);
            return response.data;
        } catch (error) {
            console.log("Error fetching movie by ID:", error);
            throw error;
        }
    },

    createMovie: async (movie: MovieDto): Promise<void> => {
        try {
            await axios.post<number>(`${API_BASE_URL}/movie`, movie);
        } catch (error) {
            console.log("Error creating movie:", error);
            throw error;
        }
    },

    editMovie: async (movie: MovieDto): Promise<void> => {
        try {
            await axios.put(`${API_BASE_URL}/movie/${movie.id}`, movie);
        } catch (error) {
            console.log("Error editing movie:", error);
            throw error;
        }
    },

    deleteMovie: async (movieId: string): Promise<void> => {
        try {
            await axios.delete(`${API_BASE_URL}/movie/${movieId}`);
        } catch (error) {
            console.log("Error deleting movie:", error);
            throw error;
        }
    }

}


export default apiConnector;