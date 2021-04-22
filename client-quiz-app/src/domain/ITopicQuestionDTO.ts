import { IBaseDomain } from '../base/IBaseDomain';
import { QuestionType } from '../base/QuestionType';
import { IQuestionAnswerDTO } from './IQuestionAnswerDTO';
import { IQuizTopicDTO } from './IQuizTopicDTO';
import { ISavedQuestionDTO } from './ISavedQuestionDTO';
import { ITeamAnswerDTO } from './ITeamAnswerDTO';

export interface ITopicQuestionDTO extends IBaseDomain {
    quizTopicId: string;
    quizTopic?: IQuizTopicDTO;

    question: string;
    text: string;
    points: number;
    type: QuestionType;

    questionAnswers?: IQuestionAnswerDTO[];
    teamAnswers?: ITeamAnswerDTO[];
    savedQuestions?: ISavedQuestionDTO[];
}