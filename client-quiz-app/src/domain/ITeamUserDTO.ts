import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';
import { IFeedbackDTO } from './IFeedbackDTO';
import { ITeamDTO } from './ITeamDTO';

export interface ITeamUserDTO extends IBaseDomain {
    teamId: string;
    team?: ITeamDTO;

    appuserId: string;
    appUser?: IAppUserDTO;

    feedback?: IFeedbackDTO[];
}