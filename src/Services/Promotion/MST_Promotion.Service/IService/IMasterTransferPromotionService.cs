using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;

namespace MST_Promotion.Services
{
    public interface IMasterTransferPromotionService : BaseInterfaceService
    {
        Task<MasterTransferPromotionDTO> CreateMasterTransferPromotionAsync(MasterTransferPromotionDTO input);
        Task<MasterTransferPromotionPaging> GetMasterTransferPromotionListAsync(MasterTransferPromotionListFilter filter, PageParam pageParam, MasterTransferPromotionSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterTransferPromotionDTO> GetMasterTransferPromotionDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<MasterTransferPromotionDTO> UpdateMasterTransferPromotionAsync(Guid id, MasterTransferPromotionDTO input);
        Task DeleteMasterTransferPromotionAsync(Guid id);

        Task<MasterTransferPromotionItemPaging> GetMasterTransferPromotionItemListAsync(Guid masterTransferPromotionID, PageParam pageParam, MasterTransferPromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<MasterTransferPromotionItemDTO>> UpdateMasterTransferPromotionItemListAsync(Guid masterTransferPromotionID, List<MasterTransferPromotionItemDTO> inputs);
        Task<MasterTransferPromotionItemDTO> UpdateMasterTransferPromotionItemAsync(Guid masterTransferPromotionID, Guid masterTransferPromotionItemID, MasterTransferPromotionItemDTO input);
        Task DeleteMasterTransferPromotionItemAsync(Guid id);
        Task<List<MasterTransferPromotionItemDTO>> CreateMasterTransferPromotionItemFromMaterialAsync(Guid masterTransferPromotionID, List<PromotionMaterialDTO> inputs);
        Task<List<MasterTransferPromotionItemDTO>> CreateSubMasterTransferPromotionItemFromMaterialAsync(Guid masterTransferPromotionID, Guid mainMasterTransferPromotionItemID, List<PromotionMaterialDTO> inputs);

        Task<List<ModelListDTO>> GetMasterTransferPromotionItemModelListAsync(Guid masterTransferPromotionItemID, CancellationToken cancellationToken = default);
        Task<List<ModelListDTO>> AddMasterTransferPromotionItemModelListAsync(Guid masterTransferPromotionItemID, List<ModelListDTO> inputs);

        //Free Item
        Task<MasterTransferPromotionFreeItemPaging> GetMasterTransferPromotionFreeItemListAsync(Guid masterTransferPromotionID,PageParam pageParam, MasterTransferPromotionFreeItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterTransferPromotionFreeItemDTO> CreateMasterTransferPromotionFreeItemAsync(Guid masterTransferPromotionID,MasterTransferPromotionFreeItemDTO input);
        Task<List<MasterTransferPromotionFreeItemDTO>> UpdateMasterTransferPromotionFreeItemListAsync(Guid masterTransferPromotionID, List<MasterTransferPromotionFreeItemDTO> inputs);
        Task<MasterTransferPromotionFreeItemDTO> UpdateMasterTransferPromotionFreeItemAsync(Guid masterTransferPromotionID, Guid masterTransferPromotionFreeItemID, MasterTransferPromotionFreeItemDTO input);
        Task DeleteMasterTransferPromotionFreeItemAsync(Guid id);

        Task<List<ModelListDTO>> GetMasterTransferPromotionFreeItemModelListAsync(Guid masterTransferPromotionFreeItemID, CancellationToken cancellationToken = default);
        Task<List<ModelListDTO>> AddMasterTransferPromotionFreeItemModelListAsync(Guid masterTransferPromotionFreeItemID, List<ModelListDTO> inputs);

        //Credit Card Item
        Task<MasterTransferCreditCardItemPaging> GetMasterTransferCreditCardItemAsync(Guid masterTransferPromotionID,PageParam pageParam, MasterTransferCreditCardItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<MasterTransferCreditCardItemDTO>> UpdateMasterTransferCreditCardItemListAsync(Guid masterTransferPromotionID,List<MasterTransferCreditCardItemDTO> inputs);
        Task<MasterTransferCreditCardItemDTO> UpdateMasterTransferCreditCardItemAsync(Guid masterTransferPromotionID, Guid masterTransferCreditCardItemID, MasterTransferCreditCardItemDTO input);
        Task DeleteMasterTransferCreditCardItemAsync(Guid id);
        Task<List<MasterTransferCreditCardItemDTO>> CreateMasterTransferCreditCardItemsAsync(Guid masterTransferPromotionID, List<PromotionMaterialDTO> inputs);

        Task<MasterTransferPromotionDTO> CloneMasterTransferPromotionAsync(Guid id);
        Task<CloneMasterPromotionConfirmDTO> GetCloneMasterTransferPromotionConfirmAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
