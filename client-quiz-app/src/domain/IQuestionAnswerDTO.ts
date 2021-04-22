import { IBaseDomain } from '../base/IBaseDomain';
import { ITeamAnswerDTO } from './ITeamAnswerDTO';
import { ITopicQuestionDTO } from './ITopicQuestionDTO';

export interface IQuestionAnswerDTO extends IBaseDomain {
    topicQuestionId: string;
    topicquestion?: ITopicQuestionDTO;

    answer: string;
    correct: boolean;

    teamAnswers?: ITeamAnswerDTO[];
}