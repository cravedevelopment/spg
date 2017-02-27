import { SPG.WebappPage } from './app.po';

describe('spg.webapp App', () => {
  let page: SPG.WebappPage;

  beforeEach(() => {
    page = new SPG.WebappPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
