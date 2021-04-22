import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';

export interface IUserFriendDTO extends IBaseDomain {
    recipientId: string;
    recipient?: IAppUserDTO;

    appUserId: string;
    appUser?: IAppUserDTO;

    pending: boolean;
    accepted: boolean;

}