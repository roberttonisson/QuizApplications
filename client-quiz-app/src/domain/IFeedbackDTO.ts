import { IBaseDomain } from '../base/IBaseDomain';
import { IQuizDTO } from './IQuizDTO';
import { ITeamUserDTO } from './ITeamUserDTO';

export interface IFeedbackDTO extends IBaseDomain {
    quizId: string;
    quiz?: IQuizDTO;
    teamUserId: string;
    teamUser?: ITeamUserDTO;

    text?: number;
    year: number;

}