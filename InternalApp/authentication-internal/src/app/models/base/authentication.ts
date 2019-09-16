export interface Authentication {
  expiration: Date;
  invalidBefore: Date;

  token: string;
  refreshToken: string;
  username: string;
}
