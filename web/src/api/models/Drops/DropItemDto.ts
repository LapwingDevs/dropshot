import { ClothesUnitOfMeasure } from '../Products/ClothesUnitOfMeasure';

export interface DropItemDto {
  dropItemId: number;
  variantId: number;
  productId: number;
  productName: string;
  unitOfSize: ClothesUnitOfMeasure;
  size: number;
}
