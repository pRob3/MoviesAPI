using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MoviesAPI.DTOs;
using MoviesAPI.DTOs.Account;
using MoviesAPI.Entities;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreateDTO, Genre>().ReverseMap();
            
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreateDTO, Actor>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreateDTO, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto => 
                    geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));

            CreateMap<Movie, MovieDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapMoviesGenres))                
                .ForMember(x => x.MovieTheaters, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.Actors, options => options.MapFrom(MapMovieActors));

            CreateMap<IdentityUser, UserDTO>().ReverseMap();
        }

        private List<ActorsMovieDTO> MapMovieActors(Movie movie, MovieDTO movieDto)
        {
            var result = new List<ActorsMovieDTO>();

            if (movie.MoviesActors == null) { return result; }
            foreach (var movieActors in movie.MoviesActors)
            {
                result.Add(new ActorsMovieDTO() { 
                    Id = movieActors.ActorId,
                    Name = movieActors.Actor.Name,
                    Character = movieActors.Character,
                    Picture = movieActors.Actor.Picture,
                    Order = movieActors.Order
                });
            }
            return result;
        }

        private List<GenreDTO> MapMoviesGenres(Movie movie, MovieDTO movieDto)
        {
            var result = new List<GenreDTO>();
            
            if (movie.MoviesGenres == null) { return result; }
            foreach (var genre in movie.MoviesGenres)
            {
                result.Add(new GenreDTO() { Id = genre.GenreId, Name = genre.Genre.Name });
            }
            return result;
        }

        private List<MovieTheaterDTO> MapMovieTheatersMovies(Movie movie, MovieDTO movieDto)
        {
            var result = new List<MovieTheaterDTO>();

            if (movie.MovieTheatersMovies == null) { return result; }
            foreach (var movieTheaterMovies in movie.MovieTheatersMovies)
            {
                result.Add(new MovieTheaterDTO() { 
                    Id = movieTheaterMovies.MovieTheaterId, 
                    Name = movieTheaterMovies.MovieTheater.Name, 
                    Latitude = movieTheaterMovies.MovieTheater.Location.Y,
                    Longitude = movieTheaterMovies.MovieTheater.Location.X,
                });
            }
            return result;
        }

        private List<MoviesGenres> MapMoviesGenres(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();
            if (movieCreateDTO.GenresIds == null) { return result; }

            foreach (var id in movieCreateDTO.GenresIds)
            {
                result.Add(new MoviesGenres() { GenreId = id });
            }
            return result;
        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();
            if (movieCreateDTO.MovieTheatersIds == null) { return result; }

            foreach (var id in movieCreateDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheatersMovies() { MovieTheaterId = id });
            }
            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (movieCreateDTO.Actors == null) { return result; }

            foreach (var actor in movieCreateDTO.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character });
            }
            return result;
        }
    }
}
