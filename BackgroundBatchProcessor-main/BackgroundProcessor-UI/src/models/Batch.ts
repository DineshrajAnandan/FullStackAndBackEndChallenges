export interface Batch {
    batchId: number;
    totalNumbers: number;
    processedNumbers: number;
    batchTotal: number;
    batchGroupId: number;
    batchGroup: BatchGroup;
}

export interface BatchGroup {
    groupId: number;
    isComplete: boolean;
}