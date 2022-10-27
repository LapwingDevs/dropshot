import { CreateDropItemDto } from './CreateDropItemDto';

export interface AddDropRequest {
  name: string;
  description: string;
  startDateTime: string;
  endDateTime: string;
  dropItems: CreateDropItemDto[];
}
