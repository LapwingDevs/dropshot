import { ClothesUnitOfMeasure } from './ClothesUnitOfMeasure';
import { VariantOnProductDto } from './VariantOnProductDto';

export interface ProductDetailsDto {
  id: number;
  name: string;
  description: string;
  price: number;
  unitOfSize: ClothesUnitOfMeasure;
  variants: VariantOnProductDto[];
}
