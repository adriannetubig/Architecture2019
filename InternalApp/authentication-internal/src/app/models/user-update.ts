import { Role } from './role';
import { User } from './user';

export interface UserUpdate {
  roles: Role[];
  user: User;
}
