import { IQuizDTO } from "../IQuizDTO";

export interface IQuizTopicWithCounter{
    quizId: string;
    topic: string;
    timeLimit: number;
    counter: number;

}