import { IBaseDomain } from '../base/IBaseDomain';
import { IQuestionAnswerDTO } from './IQuestionAnswerDTO';
import { ITeamDTO } from './ITeamDTO';
import { ITopicQuestionDTO } from './ITopicQuestionDTO';

export interface ITeamAnswerDTO extends IBaseDomain {
    teamId: string;
    team?: ITeamDTO;
    questionAnswerId: string;
    questionAnswerID?: IQuestionAnswerDTO;
    topicQuestionId: string;
    topicQuestion?: ITopicQuestionDTO;

    answer?: string;
    correct: boolean;
    points?: number;
}