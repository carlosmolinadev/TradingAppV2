
using Riok.Mapperly.Abstractions;
using TradingAppMvc.Domain.Entities;
using TradingAppMvc.Models.Requests;

namespace TradingAppMvc.Models.Mappings;

// Enums of source and target have different numeric values -> use ByName strategy to map them
// [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
[Mapper]
public partial class ApiKeyMapper
{
    [MapProperty(nameof(ApiKeyRequest.UserId), "madre")]
    public partial ApiKey MapApiKeyToDto(ApiKeyRequest apiKey);
}