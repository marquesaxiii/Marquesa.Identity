﻿using System.Net;
using Mapster;

namespace Identity.Core.DataAccess.Queries.Handler.Get;

public class GetAddressHandler : QueryBaseHandler, IRequestHandler<GetAddressQuery, QueryResponse<AddressResponse>>
{
    public GetAddressHandler(MarquesaSystemContext context,IDataLayer dataLayer)
    {
        _context = context;
        _dataLayer = dataLayer;
    }

    public async Task<QueryResponse<AddressResponse>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Guid == $"{request.Guid}", CancellationToken.None);

        if (address is null)
        {
            return new()
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Message = "Address not found",
            };
        }

        return new()
        {
            HttpStatusCode = HttpStatusCode.Accepted,
            Message = "Address found",
            Response = address.Adapt<AddressResponse>()
        };


    }
}