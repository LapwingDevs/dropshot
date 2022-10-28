import { CreateDropItemDto } from './CreateDropItemDto';

export interface AddDropRequest {
  name: string;
  description: string;
  startDateTime: Date;
  endDateTime: Date;
  dropItems: CreateDropItemDto[];
}
