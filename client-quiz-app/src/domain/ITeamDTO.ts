import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';
import { IQuizDTO } from './IQuizDTO';
import { IQuizInvitationDTO } from './IQuizInvitationDTO';
import { ITeamAnswerDTO } from './ITeamAnswerDTO';
import { ITeamUserDTO } from './ITeamUserDTO';

export interface ITeamDTO extends IBaseDomain {
    quizId: string;
    quiz?: IQuizDTO;

    appUserId: string;
    appUser?: IAppUserDTO;

    teamUsers?: ITeamUserDTO[];
    teamAnswers?: ITeamAnswerDTO[];
    quizInvitations?: IQuizInvitationDTO[];
}