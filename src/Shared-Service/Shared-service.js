import { BehaviorSubject } from 'rxjs';

const playListSubject = new BehaviorSubject({playlistId: 'PLogA9DP2_vSekxHP73PXaKD6nbOK56CJw'});
const searchValueSubject = new BehaviorSubject({searchValue: ''});
const pipSubject = new BehaviorSubject({togglePip: false, video: null});

export const playListIdSharedService = {
    setData: (p) => playListSubject.next({playlistId: p}),
    clearData: () =>playListSubject.next(),
    getData: () => playListSubject.asObservable()
};

export const searchValueSharedService = {
    setData: (s) =>  searchValueSubject.next({ searchValue: s }),
    clearData: () => searchValueSubject.next(),
    getData: () =>   searchValueSubject.asObservable()
};

export const pipSharedService = {
    setData: (p, v) =>  pipSubject.next({ togglePip: p, video: v }),
    clearData: () => pipSubject.next(),
    getData: () =>   pipSubject.asObservable()
};