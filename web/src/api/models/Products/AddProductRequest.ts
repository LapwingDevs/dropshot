import { ClothesUnitOfMeasure } from './ClothesUnitOfMeasure';

export interface AddProductRequest {
  name: string;
  description: string;
  price: number;
  unitOfSize: ClothesUnitOfMeasure;
}
