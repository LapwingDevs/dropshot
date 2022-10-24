import { DropItem } from './DropItem';

export interface DropDetailsDto {
  id: number;
  name: string;
  description: string;
  startDateTime: string;
  endDateTime: string;
  dropItems: DropItem[];
}
