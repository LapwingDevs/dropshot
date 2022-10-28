import { ClothesUnitOfMeasure } from '../Products/ClothesUnitOfMeasure';

export interface VariantDto {
  variantId: number;
  productId: number;
  productName: string;
  unitOfSize: ClothesUnitOfMeasure;
  size: number;
}
