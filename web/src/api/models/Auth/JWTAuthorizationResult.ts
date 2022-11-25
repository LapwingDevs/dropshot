import { Result } from './Result';

export interface JWTAuthorizationResult extends Result {
  token: string;
}
