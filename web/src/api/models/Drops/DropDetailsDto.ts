import { DropItemDto } from './DropItemDto';

export interface DropDetailsDto {
  id: number;
  name: string;
  description: string;
  startDateTime: string;
  endDateTime: string;
  availableDropItems: DropItemDto[];
  reservedDropItems: DropItemDto[];
}
