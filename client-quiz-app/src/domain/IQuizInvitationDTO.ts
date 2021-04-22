import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';
import { ITeamDTO } from './ITeamDTO';

export interface IQuizInvitationDTO extends IBaseDomain {
    teamId: string;
    team?: ITeamDTO;

    pending: boolean;
    accepted: boolean;

    appUserId: string;
    appUser?: IAppUserDTO;
}