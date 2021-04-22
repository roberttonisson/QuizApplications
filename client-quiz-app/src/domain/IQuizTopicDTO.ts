import { IBaseDomain } from '../base/IBaseDomain';
import { IQuizDTO } from './IQuizDTO';
import { ITopicQuestionDTO } from './ITopicQuestionDTO';

export interface IQuizTopicDTO extends IBaseDomain {
    quizId: string;
    quiz?: IQuizDTO;

    topic: string;
    timeLimit: number;

    topicQuestions?: ITopicQuestionDTO[];
}