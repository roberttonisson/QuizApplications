import { IBaseDomain } from '../base/IBaseDomain';
import { IAppUserDTO } from './identity/IAppUserDTO';
import { IFeedbackDTO } from './IFeedbackDTO';
import { IQuizTopicDTO } from './IQuizTopicDTO';
import { ITeamDTO } from './ITeamDTO';

export interface IQuizDTO extends IBaseDomain {
    appUserId: string;
    appUser?: IAppUserDTO;

    title: string;
    start: Date;
    finished: boolean;

    quizTopics?: IQuizTopicDTO[];
    feedback?: IFeedbackDTO[];
    teams?: ITeamDTO[];
}