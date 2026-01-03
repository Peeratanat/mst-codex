using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using PagingExtensions;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;

namespace MST_Promotion.Services
{
    public interface IMasterSalePromotionService : BaseInterfaceService
    {
        Task<MasterSalePromotionDTO> CreateMasterSalePromotionAsync(MasterSalePromotionDTO input);
        Task<MasterSalePromotionPaging> GetMasterSalePromotionListAsync(MasterSalePromotionListFilter filter, PageParam pageParam, MasterSalePromotionSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterSalePromotionDTO> GetMasterSalePromotionDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<MasterSalePromotionDTO> UpdateMasterSalePromotionAsync(Guid id, MasterSalePromotionDTO input);
        Task<MasterSalePromotionDTO> CheckActiveMasterSalePromotionAsync(Guid id, MasterSalePromotionDTO input);
        Task DeleteMasterSalePromotionAsync(Guid id);
        Task<MasterSalePromotionItemPaging> GetMasterSalePromotionItemListAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<MasterSalePromotionItemDTO>> UpdateMasterSalePromotionItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionItemDTO> inputs);
        Task<MasterSalePromotionItemDTO> UpdateMasterSalePromotionItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionItemID, MasterSalePromotionItemDTO input);
        Task DeleteMasterSalePromotionItemAsync(Guid id);
        Task<List<MasterSalePromotionItemDTO>> CreateMasterSalePromotionItemFromMaterialAsync(Guid masterSalePromotionID, List<PromotionMaterialDTO> inputs);
        Task<List<MasterSalePromotionItemDTO>> CreateSubMasterSalePromotionItemFromMaterialAsync(Guid masterSalePromotionID, Guid mainMasterSalePromotionItemID, List<PromotionMaterialDTO> inputs);
        Task<List<ModelListDTO>> GetMasterSalePromotionItemModelListAsync(Guid masterSalePromotionItemID, CancellationToken cancellationToken = default);
        Task<List<ModelListDTO>> AddMasterSalePromotionItemModelListAsync(Guid masterSalePromotionItemID, List<ModelListDTO> inputs);
        //Free Item
        Task<MasterSalePromotionFreeItemPaging> GetMasterSalePromotionFreeItemListAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionFreeItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterSalePromotionFreeItemDTO> CreateMasterSalePromotionFreeItemAsync(Guid masterSalePromotionID, MasterSalePromotionFreeItemDTO input);
        Task<List<MasterSalePromotionFreeItemDTO>> UpdateMasterSalePromotionFreeItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionFreeItemDTO> inputs);
        Task<MasterSalePromotionFreeItemDTO> UpdateMasterSalePromotionFreeItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionFreeItemID, MasterSalePromotionFreeItemDTO input);
        Task DeleteMasterSalePromotionFreeItemAsync(Guid id);
        Task<List<ModelListDTO>> GetMasterSalePromotionFreeItemModelListAsync(Guid masterSalePromotionFreeItemID, CancellationToken cancellationToken = default);
        Task<List<ModelListDTO>> AddMasterSalePromotionFreeItemModelListAsync(Guid masterSalePromotionFreeItemID, List<ModelListDTO> inputs);
        //Credit Card Item
        Task<MasterSalePromotionCreditCardItemPaging> GetMasterSalePromotionCreditCardItemAsync(Guid masterSalePromotionID, PageParam pageParam, MasterSalePromotionCreditCardItemSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<MasterSalePromotionCreditCardItemDTO>> UpdateMasterSalePromotionCreditCardItemListAsync(Guid masterSalePromotionID, List<MasterSalePromotionCreditCardItemDTO> inputs);
        Task<MasterSalePromotionCreditCardItemDTO> UpdateMasterSalePromotionCreditCardItemAsync(Guid masterSalePromotionID, Guid masterSalePromotionCreditItemID, MasterSalePromotionCreditCardItemDTO input);
        Task DeleteMasterSalePromotionCreditCardItemAsync(Guid id);
        Task<List<MasterSalePromotionCreditCardItemDTO>> CreateMasterSalePromotionCreditCardItemsAsync(Guid masterSalePromotionID, List<EDCFeeDTO> inputs);
        Task<List<MasterSalePromotionCreditCardItemDTO>> CreditCardItemsFromPromotionMaterialListAsync(Guid masterSalePromotionID, List<PromotionMaterialDTO> inputs);
        Task<MasterSalePromotionDTO> CloneMasterSalePromotionAsync(Guid id);
        Task<CloneMasterPromotionConfirmDTO> GetCloneMasterSalePromotionConfirmAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
