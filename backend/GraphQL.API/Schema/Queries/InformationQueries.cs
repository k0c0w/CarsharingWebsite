using System.Globalization;
using Contracts.NewsService;
using Domain.Entities;
using Features.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	public async Task<IEnumerable<Document>> GetDocuments([FromServices] CarsharingContext context)
	{
		var documents = await context.WebsiteDocuments.ToArrayAsync();

		//TODO: сделать vm для них
		return documents;
	}

	public async Task<IEnumerable<PostDto>> GetNews(
		ISender sender,
		int page = 1, 
		int limit = 20, 
		bool byDescending = true)
	{
		if (page <= 0 || limit <= 0) 
			return Array.Empty<PostDto>();

		var newsQuery = await sender.Send(new GetPaginatedPostsQuery()
		{
			Page = page,
			Limit = limit,
			ByDescending = byDescending
		});

		if (!newsQuery || !newsQuery.IsSuccess)
			throw new GraphQLException("Not found.");

		//TODO: сделать vm для них
		return newsQuery.Value!;
	}
}